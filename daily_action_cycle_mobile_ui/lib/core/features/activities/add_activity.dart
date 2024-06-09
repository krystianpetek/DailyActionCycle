import 'package:daily_action_cycle_mobile_ui/core/entities/activity.dart';
import 'package:daily_action_cycle_mobile_ui/core/errors/failure.dart';
import 'package:daily_action_cycle_mobile_ui/core/repositories/activity_repository.dart';
import 'package:daily_action_cycle_mobile_ui/core/usecase/usecase.dart';
import 'package:daily_action_cycle_mobile_ui/data/models/add_activity_model.dart';
import 'package:dartz/dartz.dart';

class AddActivity implements UseCase<void, AddActivityModel> {
  final ActivityRepository repository;

  AddActivity(this.repository);

  @override
  Future<Either<Failure, void>> call(AddActivityModel activity) async {
    return await repository.addActivity(activity);
  }
}
