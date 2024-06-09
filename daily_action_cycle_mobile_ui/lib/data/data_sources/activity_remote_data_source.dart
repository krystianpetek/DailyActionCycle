import 'dart:io';

import 'package:daily_action_cycle_mobile_ui/core/errors/exception.dart';
import 'package:daily_action_cycle_mobile_ui/data/models/activity_model.dart';
import 'package:daily_action_cycle_mobile_ui/core/config.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

abstract class ActivityRemoteDataSource {
  Future<List<ActivityModel>> getActivities();
  Future<void> addActivity(ActivityModel activity);
  Future<void> updateActivity(ActivityModel activity);
  Future<void> deleteActivity(String id);
}

class ActivityRemoteDataSourceImpl implements ActivityRemoteDataSource {
  final http.Client client;

  ActivityRemoteDataSourceImpl({required this.client});

  @override
  Future<List<ActivityModel>> getActivities() async {
    try {
      var httpClient = new HttpClient();
      httpClient.badCertificateCallback =
          ((X509Certificate cert, String host, int port) => true);

      final response =
          await client.get(Uri.parse('${Config.baseUrl}/activities'));

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
  Future<void> addActivity(ActivityModel activity) async {
    final response = await client.post(
      Uri.parse('${Config.baseUrl}/activities'),
      headers: {'Content-Type': 'application/json'},
      body: json.encode(activity.toJson()),
    );

    if (response.statusCode != 201) {
      _handleError(response);
      throw ServerException();
    }
  }

  @override
  Future<void> updateActivity(ActivityModel activity) async {
    final response = await client.put(
      Uri.parse('${Config.baseUrl}/activities/${activity.id}'),
      headers: {'Content-Type': 'application/json'},
      body: json.encode(activity.toJson()),
    );

    if (response.statusCode != 200) {
      _handleError(response);
      throw ServerException();
    }
  }

  @override
  Future<void> deleteActivity(String id) async {
    final response =
        await client.delete(Uri.parse('${Config.baseUrl}/activities/$id'));

    if (response.statusCode != 200) {
      _handleError(response);
      throw ServerException();
    }
  }

  void _handleError(http.Response response) {
    print('Error: ${response.statusCode}');
    print('Headers: ${response.headers}');
    print('Body: ${response.body}');
  }
}
