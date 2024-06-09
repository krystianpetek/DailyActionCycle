import 'package:equatable/equatable.dart';
import 'activity.dart';
import 'action_template.dart';

class Day extends Equatable {
  final String id;
  final DateTime date;
  final String actionTemplateId;
  final ActionTemplate? actionTemplate;
  final List<Activity> tasks;

  Day({
    required this.id,
    required this.date,
    required this.actionTemplateId,
    this.actionTemplate,
    required this.tasks,
  });

  @override
  List<Object?> get props =>
      [id, date, actionTemplateId, actionTemplate, tasks];
}
