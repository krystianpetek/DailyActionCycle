import 'package:bloc/bloc.dart';
import 'package:daily_action_cycle_mobile_ui/core/entities/activity.dart';
import 'package:daily_action_cycle_mobile_ui/core/errors/cache_failure.dart';
import 'package:daily_action_cycle_mobile_ui/core/errors/failure.dart';
import 'package:daily_action_cycle_mobile_ui/core/errors/server_failure.dart';
import 'package:daily_action_cycle_mobile_ui/core/features/activities/add_activity.dart';
import 'package:daily_action_cycle_mobile_ui/core/features/activities/get_activities.dart';
import 'package:daily_action_cycle_mobile_ui/core/features/activities/update_activity.dart';
import 'package:daily_action_cycle_mobile_ui/core/usecase/usecase.dart';
import 'package:daily_action_cycle_mobile_ui/data/models/add_activity_model.dart';
import 'package:dartz/dartz.dart';
import 'package:equatable/equatable.dart';
import 'package:flutter/foundation.dart';

part 'activity_event.dart';
part 'activity_state.dart';

class ActivityBloc extends Bloc<ActivityEvent, ActivityState> {
  final GetActivities getActivities;
  final AddActivity addActivity;
  final UpdateActivity updateActivity;

  ActivityBloc(
      {required this.getActivities,
      required this.addActivity,
      required this.updateActivity})
      : super(ActivityInitial()) {
    on<LoadActivities>(_onLoadActivities);
    on<AddActivityEvent>(_onAddActivity);
    on<UpdateActivityEvent>(_onUpdateActivity);
  }

  void _onLoadActivities(
      LoadActivities event, Emitter<ActivityState> emit) async {
    emit(ActivityLoading());
    final failureOrActivities = await getActivities(NoParams());
    emit(_eitherLoadedOrErrorState(failureOrActivities));
  }

  void _onAddActivity(
      AddActivityEvent event, Emitter<ActivityState> emit) async {
    emit(ActivityLoading());
    final failureOrSuccess = await addActivity(event.activity);
    if (failureOrSuccess.isRight()) {
      final failureOrActivities = await getActivities(NoParams());
      emit(_eitherLoadedOrErrorState(failureOrActivities));
    } else {
      emit(const ActivityError(message: 'Failed to add activity'));
    }
  }

  void _onUpdateActivity(
      UpdateActivityEvent event, Emitter<ActivityState> emit) async {
    emit(ActivityLoading());
    final failureOrSuccess = await updateActivity(event.activity);
    if (failureOrSuccess.isRight()) {
      final failureOrActivities = await getActivities(NoParams());
      emit(_eitherLoadedOrErrorState(failureOrActivities));
    } else {
      emit(const ActivityError(message: 'Failed to update activity'));
    }
  }

  //   @override
  // Stream<ActivityState> mapEventToState(ActivityEvent event) async* {
  //   if (event is LoadActivities) {
  //     yield ActivityLoading();
  //     final failureOrActivities = await getActivities(NoParams());
  //     yield* _eitherLoadedOrErrorState(failureOrActivities);
  //   }
  // }

  ActivityState _eitherLoadedOrErrorState(
    Either<Failure, List<Activity>> either,
  ) {
    return either.fold(
      (failure) => ActivityError(message: _mapFailureToMessage(failure)),
      (activities) => ActivityLoaded(activities: activities),
    );
  }

  // Stream<ActivityState> _eitherLoadedOrErrorState(
  //   Either<Failure, List<Activity>> either,
  // ) async* {
  //   yield either.fold(
  //     (failure) => ActivityError(message: _mapFailureToMessage(failure)),
  //     (activities) => ActivityLoaded(activities: activities),
  //   );
  // }

  String _mapFailureToMessage(Failure failure) {
    switch (failure.runtimeType) {
      case ServerFailure:
        return 'Server Failure';
      case CacheFailure:
        return 'Cache Failure';
      default:
        return 'Unexpected Error';
    }
  }
}
