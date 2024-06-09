import 'package:daily_action_cycle_mobile_ui/core/entities/activity.dart';
import 'package:daily_action_cycle_mobile_ui/core/errors/cache_failure.dart';
import 'package:daily_action_cycle_mobile_ui/core/errors/exception.dart';
import 'package:daily_action_cycle_mobile_ui/core/errors/failure.dart';
import 'package:daily_action_cycle_mobile_ui/core/errors/server_failure.dart';
import 'package:daily_action_cycle_mobile_ui/core/repositories/activity_repository.dart';
import 'package:daily_action_cycle_mobile_ui/data/data_sources/activity_local_data_source.dart';
import 'package:daily_action_cycle_mobile_ui/data/data_sources/activity_remote_data_source.dart';
import 'package:daily_action_cycle_mobile_ui/data/models/activity_model.dart';
import 'package:dartz/dartz.dart';

class ActivityRepositoryImpl implements ActivityRepository {
  final ActivityRemoteDataSource remoteDataSource;
  final ActivityLocalDataSource localDataSource;

  ActivityRepositoryImpl({required this.remoteDataSource, required this.localDataSource});

  @override
  Future<Either<Failure, List<Activity>>> getActivities() async {
    try {
      final remoteActivities = await remoteDataSource.getActivities();
      localDataSource.cacheActivities(remoteActivities);
      return Right(remoteActivities);
    } on ServerException catch (e) {
      print('ServerException in getActivities: $e');
      try {
        final localActivities = await localDataSource.getCachedActivities();
        return Right(localActivities);
      } on CacheException catch (e) {
       print('CacheException in getActivities: $e');
        return Left(CacheFailure('Failed to fetch data from cache'));
      }
    } catch (e) {
      print('Unexpected exception in getActivities: $e');
      return Left(ServerFailure('Failed to fetch data from server'));
    }
  }

  @override
  Future<Either<Failure, void>> addActivity(Activity activity) async {
    try {
      final activityModel = ActivityModel(
        id: activity.id,
        title: activity.title,
        description: activity.description,
        createdAt: activity.createdAt,
        dueDate: activity.dueDate,
        isCompleted: activity.isCompleted,
        isNotified: activity.isNotified,
        updatedAt: activity.updatedAt,
        isDeleted: activity.isDeleted,
        deletedAt: activity.deletedAt,
      );

      await remoteDataSource.addActivity(activityModel);
      return Right(null);
    } on ServerException catch (e) {
      print('ServerException in addActivity: $e');
      return Left(ServerFailure('Failed to add activity'));
    } catch (e) {
     print('Unexpected exception in addActivity: $e');
      return Left(ServerFailure('Failed to add activity'));
    }
  }

  @override
  Future<Either<Failure, void>> updateActivity(Activity activity) async {
    try {
      final activityModel = ActivityModel(
        id: activity.id,
        title: activity.title,
        description: activity.description,
        createdAt: activity.createdAt,
        dueDate: activity.dueDate,
        isCompleted: activity.isCompleted,
        isNotified: activity.isNotified,
        updatedAt: activity.updatedAt,
        isDeleted: activity.isDeleted,
        deletedAt: activity.deletedAt,
      );

      await remoteDataSource.updateActivity(activityModel);
      return Right(null);
    } on ServerException catch (e) {
      print('ServerException in updateActivity: $e');
      return Left(ServerFailure('Failed to update activity'));
    } catch (e) {
      print('Unexpected exception in updateActivity: $e');
      return Left(ServerFailure('Failed to update activity'));
    }
  }

  @override
  Future<Either<Failure, void>> deleteActivity(String id) async {
    try {
      await remoteDataSource.deleteActivity(id);
      return Right(null);
    } on ServerException catch (e) {
      print('ServerException in deleteActivity: $e');
      return Left(ServerFailure('Failed to delete activity'));
    } catch (e) {
      print('Unexpected exception in deleteActivity: $e');
      return Left(ServerFailure('Failed to delete activity'));
    }
  }
}
