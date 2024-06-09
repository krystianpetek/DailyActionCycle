// // ignore_for_file: public_member_api_docs, sort_constructors_first
// import 'package:daily_action_cycle_mobile_ui/core/entities/day.dart';
// import 'package:daily_action_cycle_mobile_ui/core/repositories/day_repository.dart';
// import 'package:daily_action_cycle_mobile_ui/data/repositories/day/day_api.dart';
// import 'package:daily_action_cycle_mobile_ui/data/repositories/day/day_local_storage.dart';

// class DayRepositoryImpl implements DayRepository {
//   final DayApi _api;
//   final DayLocalStorage _localStorage;

//   DayRepositoryImpl({
//     required DayApi api,
//     required DayLocalStorage localStorage,
//   })  : _api = api,
//         _localStorage = localStorage;

//   @override
//   Future<List<Day>> getDays(int countOfDays) async {
//     final List<Day> cachedDays = _localStorage.loadDays();

//     if (cachedDays.isNotEmpty) {
//       return cachedDays;
//     }

//     final fetchedDays = await _api.loadDays();

//     return fetchedDays;
//   }
// }
