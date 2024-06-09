import 'package:daily_action_cycle_mobile_ui/core/entities/activity.dart';

class ActivityModel extends Activity {
  ActivityModel({
    required String id,
    required String title,
    required String description,
    required DateTime createdAt,
    required DateTime dueDate,
    bool isCompleted = false,
    bool isNotified = false,
    DateTime? updatedAt,
    bool isDeleted = false,
    DateTime? deletedAt,
  }) : super(
          id: id,
          title: title,
          description: description,
          createdAt: createdAt,
          dueDate: dueDate,
          isCompleted: isCompleted,
          isNotified: isNotified,
          updatedAt: updatedAt,
          isDeleted: isDeleted,
          deletedAt: deletedAt,
        );

  factory ActivityModel.fromJson(Map<String, dynamic> json) {
    return ActivityModel(
      id: json['id'],
      title: json['title'],
      description: json['description'],
      createdAt: DateTime.parse(json['createdAt']),
      dueDate: DateTime.parse(json['dueDate']),
      isCompleted: json['isCompleted'] ?? false,
      isNotified: json['isNotified'] ?? false,
      updatedAt:
          json['updatedAt'] != null ? DateTime.parse(json['updatedAt']) : null,
      isDeleted: json['isDeleted'] ?? false,
      deletedAt:
          json['deletedAt'] != null ? DateTime.parse(json['deletedAt']) : null,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'title': title,
      'description': description,
      'createdAt': createdAt.toIso8601String(),
      'dueDate': dueDate.toIso8601String(),
      'isCompleted': isCompleted,
      'isNotified': isNotified,
      'updatedAt': updatedAt?.toIso8601String(),
      'isDeleted': isDeleted,
      'deletedAt': deletedAt?.toIso8601String(),
    };
  }
}
