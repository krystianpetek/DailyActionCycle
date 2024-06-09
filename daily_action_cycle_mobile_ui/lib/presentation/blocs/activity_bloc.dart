import 'package:bloc/bloc.dart';
import 'package:daily_action_cycle_mobile_ui/core/entities/activity.dart';
import 'package:daily_action_cycle_mobile_ui/core/errors/cache_failure.dart';
import 'package:daily_action_cycle_mobile_ui/core/errors/failure.dart';
import 'package:daily_action_cycle_mobile_ui/core/errors/server_failure.dart';
import 'package:daily_action_cycle_mobile_ui/core/features/activities/get_activities.dart';
import 'package:daily_action_cycle_mobile_ui/core/usecase/usecase.dart';
import 'package:dartz/dartz.dart';
import 'package:equatable/equatable.dart';

part 'activity_event.dart';
part 'activity_state.dart';

class ActivityBloc extends Bloc<ActivityEvent, ActivityState> {
  final GetActivities getActivities;

  ActivityBloc({required this.getActivities}) : super(ActivityInitial()) {
    on<LoadActivities>(_onLoadActivities);
  }

  void _onLoadActivities(LoadActivities event, Emitter<ActivityState> emit) async {
    emit(ActivityLoading());
    final failureOrActivities = await getActivities(NoParams());
    emit(_eitherLoadedOrErrorState(failureOrActivities));
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
