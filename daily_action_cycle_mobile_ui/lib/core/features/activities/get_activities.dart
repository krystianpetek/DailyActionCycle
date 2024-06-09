import 'package:daily_action_cycle_mobile_ui/core/entities/activity.dart';
import 'package:daily_action_cycle_mobile_ui/core/errors/failure.dart';
import 'package:daily_action_cycle_mobile_ui/core/repositories/activity_repository.dart';
import 'package:daily_action_cycle_mobile_ui/core/usecase/usecase.dart';
import 'package:dartz/dartz.dart';

class GetActivities implements UseCase<List<Activity>, NoParams> {
  final ActivityRepository repository;

  GetActivities(this.repository);

  @override
  Future<Either<Failure, List<Activity>>> call(NoParams params) async {
    return await repository.getActivities();
  }
}
