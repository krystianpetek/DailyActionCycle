import 'package:daily_action_cycle_mobile_ui/core/entities/entity.dart';

class Habit extends Entity {
  bool? daily;

  Habit(super.id, super.name, super.description, super.createdAt,
      super.completed);
}
