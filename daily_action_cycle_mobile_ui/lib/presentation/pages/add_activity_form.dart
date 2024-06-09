import 'package:daily_action_cycle_mobile_ui/core/entities/activity.dart';
import 'package:daily_action_cycle_mobile_ui/data/models/add_activity_model.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import '../blocs/activity_bloc.dart';

class AddActivityForm extends StatefulWidget {
  @override
  _AddActivityFormState createState() => _AddActivityFormState();
}

class _AddActivityFormState extends State<AddActivityForm> {
  final _formKey = GlobalKey<FormState>();
  final _titleController = TextEditingController();
  final _descriptionController = TextEditingController();

  void _submitForm() {
    if (_formKey.currentState!.validate()) {
      final activity = AddActivityModel(
        title: _titleController.text,
        description: _descriptionController.text,
      );

      context.read<ActivityBloc>().add(AddActivityEvent(activity: activity));
      Navigator.pop(context);
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Add Activity')),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Form(
          key: _formKey,
          child: Column(
            children: [
              TextFormField(
                controller: _titleController,
                decoration: InputDecoration(labelText: 'Title'),
                validator: (value) {
                  if (value!.isEmpty) {
                    return 'Please enter a title';
                  }
                  return null;
                },
              ),
              TextFormField(
                controller: _descriptionController,
                decoration: InputDecoration(labelText: 'Description'),
                validator: (value) {
                  if (value!.isEmpty) {
                    return 'Please enter a description';
                  }
                  return null;
                },
              ),
              SizedBox(height: 16.0),
              ElevatedButton(
                onPressed: _submitForm,
                child: Text('Add Activity'),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
