import 'package:daily_action_cycle_mobile_ui/core/entities/day.dart';

abstract class DayApi {
  Future<bool> saveDays({required int countOfDays, required List<Day> days});

  Future<bool> saveDay({required Day day});

  Future<List<Day>> loadDays();

  Future<Day> loadDay(DateTime dateTime);
}

class DayApiImpl implements DayApi {
  @override
  Future<Day> loadDay(DateTime dateTime) {
    // TODO: implement loadDay
    throw UnimplementedError();
  }

  @override
  Future<List<Day>> loadDays() {
    // TODO: implement loadDays
    throw UnimplementedError();
  }

  @override
  Future<bool> saveDay({required Day day}) {
    // TODO: implement saveDay
    throw UnimplementedError();
  }

  @override
  Future<bool> saveDays({required int countOfDays, required List<Day> days}) {
    // TODO: implement saveDays
    throw UnimplementedError();
  }
}
