import 'package:daily_action_cycle_mobile_ui/core/features/activities/add_activity.dart';
import 'package:daily_action_cycle_mobile_ui/core/features/activities/get_activities.dart';
import 'package:daily_action_cycle_mobile_ui/core/features/activities/update_activity.dart';
import 'package:daily_action_cycle_mobile_ui/core/repositories/activity_repository.dart';
import 'package:daily_action_cycle_mobile_ui/data/data_sources/activity_local_data_source.dart';
import 'package:daily_action_cycle_mobile_ui/data/data_sources/activity_remote_data_source.dart';
import 'package:daily_action_cycle_mobile_ui/data/repositories/activity/activity_repository_impl.dart';
import 'package:get_it/get_it.dart';
import 'package:http/http.dart' as http;
import 'package:shared_preferences/shared_preferences.dart';
import 'presentation/blocs/activity_bloc.dart';

final sl = GetIt.instance;

Future<void> init() async {
  // BLoC
  sl.registerFactory(() => ActivityBloc(
      getActivities: sl(), addActivity: sl(), updateActivity: sl())); // Rejestracja zależności

  // Use cases
  sl.registerLazySingleton(() => GetActivities(sl()));
  sl.registerLazySingleton(() => AddActivity(sl()));
  sl.registerLazySingleton(() => UpdateActivity(sl()));

  // Repository
  sl.registerLazySingleton<ActivityRepository>(
    () => ActivityRepositoryImpl(
      remoteDataSource: sl(),
      localDataSource: sl(), // Rejestracja zależności
    ),
  );

  // Data sources
  sl.registerLazySingleton<ActivityRemoteDataSource>(
    () => ActivityRemoteDataSourceImpl(),
  );

  sl.registerLazySingleton<ActivityLocalDataSource>(
    () => ActivityLocalDataSourceImpl(sharedPreferences: sl()), // Rejestracja
  );

  // External
  final sharedPreferences = await SharedPreferences.getInstance();
  sl.registerLazySingleton(() => sharedPreferences);
  sl.registerLazySingleton(() => http.Client());
}
