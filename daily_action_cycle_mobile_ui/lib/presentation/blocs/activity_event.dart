part of 'activity_bloc.dart';

@immutable
abstract class ActivityEvent extends Equatable {
  const ActivityEvent();

  @override
  List<Object> get props => [];
}

class LoadActivities extends ActivityEvent {}

class AddActivityEvent extends ActivityEvent {
  final AddActivityModel activity;

  const AddActivityEvent({required this.activity});

  @override
  List<Object> get props => [activity];
}

class UpdateActivityEvent extends ActivityEvent {
  final Activity activity;

  const UpdateActivityEvent({required this.activity});

  @override
  List<Object> get props => [activity];
}
