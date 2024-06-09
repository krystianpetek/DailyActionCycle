import 'package:daily_action_cycle_mobile_ui/core/errors/failure.dart';
import 'package:dartz/dartz.dart';
import '../entities/action_template.dart';

abstract class ActionTemplateRepository {
  Future<Either<Failure, List<ActionTemplate>>> getActionTemplates();
  Future<Either<Failure, void>> addActionTemplate(ActionTemplate template);
  Future<Either<Failure, void>> updateActionTemplate(ActionTemplate template);
  Future<Either<Failure, void>> deleteActionTemplate(String id);
}
