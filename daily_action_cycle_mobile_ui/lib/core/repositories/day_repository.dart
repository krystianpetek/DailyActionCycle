import 'package:daily_action_cycle_mobile_ui/core/errors/failure.dart';
import 'package:dartz/dartz.dart';
import '../entities/day.dart';

abstract class DayRepository {
  Future<Either<Failure, List<Day>>> getDays(); //int countOfDays)
  Future<Either<Failure, void>> addDay(Day day);
  Future<Either<Failure, void>> updateDay(Day day);
  Future<Either<Failure, void>> deleteDay(String id);
}
