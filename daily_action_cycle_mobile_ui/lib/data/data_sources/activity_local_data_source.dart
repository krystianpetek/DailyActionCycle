import 'package:shared_preferences/shared_preferences.dart';
import 'dart:convert';

import '../../core/errors/exception.dart';
import '../models/activity_model.dart';

abstract class ActivityLocalDataSource {
  Future<List<ActivityModel>> getCachedActivities();
  Future<void> cacheActivities(List<ActivityModel> activities);
}

class ActivityLocalDataSourceImpl implements ActivityLocalDataSource {
  final SharedPreferences sharedPreferences;

  ActivityLocalDataSourceImpl({required this.sharedPreferences});

  @override
  Future<List<ActivityModel>> getCachedActivities() {
    final jsonString = sharedPreferences.getString('CACHED_ACTIVITIES');
    if (jsonString != null) {
      final List<dynamic> jsonData = json.decode(jsonString);
      return Future.value(jsonData.map((json) => ActivityModel.fromJson(json)).toList());
    } else {
      throw CacheException();
    }
  }

  @override
  Future<void> cacheActivities(List<ActivityModel> activities) {
    final jsonString = json.encode(activities.map((activity) => activity.toJson()).toList());
    sharedPreferences.setString('CACHED_ACTIVITIES', jsonString);
    return Future.value();
  }
}
