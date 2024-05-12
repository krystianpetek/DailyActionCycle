import 'package:daily_action_cycle_mobile_ui/core/entities/action_template.dart';
import 'package:daily_action_cycle_mobile_ui/core/entities/habit.dart';
import 'package:daily_action_cycle_mobile_ui/core/entities/todo.dart';

class Day {
  String? id;
  DateTime? dueDate;

  List<ToDo> tasks = [];
  List<Habit> habits = [];

  ActionTemplate? actionTemplate;
}
