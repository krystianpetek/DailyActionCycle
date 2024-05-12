import 'package:daily_action_cycle_mobile_ui/core/entities/day.dart';
import 'package:shared_preferences/shared_preferences.dart';

abstract class DayLocalStorage {
  Future<bool> saveDays({required int countOfDays, required List<Day> days});

  Future<bool> saveDay({required Day day});

  List<Day> loadDays();

  Day loadDay(DateTime dateTime);
}

class DayLocalStorageImpl implements DayLocalStorage {
  final SharedPreferences _sharedPreferences;

  DayLocalStorageImpl({required sharedPreferences})
      : _sharedPreferences = sharedPreferences;

  @override
  Day loadDay(DateTime dateTime) {
    final key = GetKeyToDay(dateTime);

    throw UnimplementedError();
    // return _sharedPreferences.getString(key);
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

  static String GetKeyToDay(DateTime day) {
    return day.day.toString();
  }

  @override
  List<Day> loadDays() {
    // TODO: implement loadDays
    throw UnimplementedError();
  }
}
