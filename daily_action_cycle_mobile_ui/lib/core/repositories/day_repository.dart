import 'package:daily_action_cycle_mobile_ui/core/entities/day.dart';

abstract class DayRepository {
  Future<List<Day>> getDays(int countOfDays);
}
