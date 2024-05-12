import 'package:daily_action_cycle_mobile_ui/core/entities/day.dart';
import 'package:daily_action_cycle_mobile_ui/core/repositories/day_repository.dart';

class GetDays {
  final DayRepository _dayRepository;

  GetDays({required DayRepository dayRepository})
      : _dayRepository = dayRepository;

  Future<List<Day>> call({int countOfDays = 1}) async {
    final days = await _dayRepository.getDays(countOfDays);
    return days;
  }
}
