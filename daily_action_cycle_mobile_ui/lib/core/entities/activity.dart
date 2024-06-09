import 'package:equatable/equatable.dart';

class Activity extends Equatable {
  final String id;
  final String title;
  final String description;
  final DateTime createdAt;
  final DateTime dueDate;
  final bool isCompleted;
  final bool isNotified;
  final DateTime? updatedAt;
  final bool isDeleted;
  final DateTime? deletedAt;

  Activity({
    required this.id,
    required this.title,
    required this.description,
    required this.createdAt,
    required this.dueDate,
    this.isCompleted = false,
    this.isNotified = false,
    this.updatedAt,
    this.isDeleted = false,
    this.deletedAt,
  });

  @override
  List<Object?> get props => [
        id,
        title,
        description,
        createdAt,
        dueDate,
        isCompleted,
        isNotified,
        updatedAt,
        isDeleted,
        deletedAt
      ];
}
