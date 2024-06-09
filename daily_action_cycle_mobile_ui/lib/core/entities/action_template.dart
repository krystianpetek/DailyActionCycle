import 'package:equatable/equatable.dart';
import 'activity.dart';

class ActionTemplate extends Equatable {
  final String id;
  final String name;
  final List<Activity> tasks;

  ActionTemplate({
    required this.id,
    required this.name,
    required this.tasks,
  });

  @override
  List<Object?> get props => [id, name, tasks];
}
