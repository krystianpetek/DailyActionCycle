import 'package:daily_action_cycle_mobile_ui/core/entities/activity.dart';
import 'package:daily_action_cycle_mobile_ui/core/errors/failure.dart';
import 'package:daily_action_cycle_mobile_ui/core/repositories/activity_repository.dart';
import 'package:daily_action_cycle_mobile_ui/core/usecase/usecase.dart';
import 'package:dartz/dartz.dart';

class UpdateActivity implements UseCase<void, Activity> {
  final ActivityRepository repository;

  UpdateActivity(this.repository);

  @override
  Future<Either<Failure, void>> call(Activity activity) async {
    return await repository.updateActivity(activity);
  }
}


// update activity rewrite

