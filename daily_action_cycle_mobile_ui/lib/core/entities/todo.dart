import 'entity.dart';

class ToDo extends Entity {
  DateTime? dueDate;
  bool? notify;

  ToDo(super.id, super.name, super.description, super.createdAt,
      super.completed);
}
