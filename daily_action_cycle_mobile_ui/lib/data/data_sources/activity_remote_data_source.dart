import 'package:daily_action_cycle_mobile_ui/core/errors/exception.dart';
import 'package:daily_action_cycle_mobile_ui/data/models/activity_model.dart';
import 'package:daily_action_cycle_mobile_ui/core/config.dart';
import 'package:daily_action_cycle_mobile_ui/data/models/add_activity_model.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

abstract class ActivityRemoteDataSource {
  Future<List<ActivityModel>> getActivities();
  Future<void> addActivity(AddActivityModel activity);
  Future<void> updateActivity(ActivityModel activity);
  Future<void> deleteActivity(String id);
}

class ActivityRemoteDataSourceImpl implements ActivityRemoteDataSource {
  @override
  Future<List<ActivityModel>> getActivities() async {
    try {
      final response =
          await http.get(Uri.parse('${Config.baseUrl}/activities'));
      if (response.statusCode == 200) {
        final List<dynamic> jsonData = json.decode(response.body);
        return jsonData.map((json) => ActivityModel.fromJson(json)).toList();
      } else {
        _handleError(response);
        throw ServerException();
      }
    } catch (e) {
      print('Exception in getActivities: $e');
      rethrow;
    }
  }

  @override
  Future<void> addActivity(AddActivityModel activity) async {
    try {
      final response = await http.post(
        Uri.parse('${Config.baseUrl}/activities'),
        headers: {'Content-Type': 'application/json'},
        body: json.encode(activity.toJson()),
      );

      if (response.statusCode != 200) {
        _handleError(response);
        throw ServerException();
      }
    } catch (e) {
      print('Exception in addActivity: $e');
      rethrow;
    }
  }

  @override
  Future<void> updateActivity(ActivityModel activity) async {
    try {
      final response = await http.put(
        Uri.parse('${Config.baseUrl}/activities/${activity.id}'),
        headers: {'Content-Type': 'application/json'},
        body: json.encode(activity.toJson()),
      );

      if (response.statusCode != 200) {
        _handleError(response);
        throw ServerException();
      }
    } catch (e) {
      print('Exception in updateActivity: $e');
      rethrow;
    }
  }

  @override
  Future<void> deleteActivity(String id) async {
    try {
      final response =
          await http.delete(Uri.parse('${Config.baseUrl}/activities/$id'));

      if (response.statusCode != 200) {
        _handleError(response);
        throw ServerException();
      }
    } catch (e) {
      print('Exception in deleteActivity: $e');
      rethrow;
    }
  }

  void _handleError(http.Response response) {
    print('Error: ${response.statusCode}');
    print('Headers: ${response.headers}');
    print('Body: ${response.body}');
  }
}
