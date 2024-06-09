import 'package:daily_action_cycle_mobile_ui/core/errors/failure.dart';
import 'package:dartz/dartz.dart';
import '../entities/activity.dart';

abstract class ActivityRepository {
  Future<Either<Failure, List<Activity>>> getActivities();
  Future<Either<Failure, void>> addActivity(Activity activity);
  Future<Either<Failure, void>> updateActivity(Activity activity);
  Future<Either<Failure, void>> deleteActivity(String id);
}
