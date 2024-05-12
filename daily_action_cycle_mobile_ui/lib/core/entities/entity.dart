import 'dart:core';

class Entity {
  String? id;
  String? name;
  String? description;
  DateTime? createdAt;
  DateTime? updatedAt;
  DateTime? deletedAt;
  bool completed = false;

  Entity(this.id, this.name, this.description, this.createdAt, this.completed);
}
