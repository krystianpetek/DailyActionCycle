class AddActivityModel {
  String title;
  String description;

  AddActivityModel({
    required this.title,
    required this.description,
  });

  Map<String, dynamic> toJson() {
    return {
      'title': title,
      'description': description,
    };
  }
}
