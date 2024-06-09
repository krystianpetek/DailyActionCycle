import 'package:daily_action_cycle_mobile_ui/core/features/activities/update_activity.dart';
import 'package:daily_action_cycle_mobile_ui/presentation/pages/add_activity_form.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import '../blocs/activity_bloc.dart';

class ActivityPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Activities'),
        actions: [
          IconButton(
            icon: Icon(Icons.refresh),
            onPressed: () {
              context.read<ActivityBloc>().add(LoadActivities());
            },
          ),
        ],
      ),
      body:
          BlocListener<ActivityBloc, ActivityState>(listener: (context, state) {
        if (state is ActivityError) {
          ScaffoldMessenger.of(context).showSnackBar(
            SnackBar(content: Text(state.message)),
          );
          // Automatically reload data after showing the error message
          context.read<ActivityBloc>().add(LoadActivities());
        }
      }, child: BlocBuilder<ActivityBloc, ActivityState>(
        builder: (context, state) {
          if (state is ActivityLoading) {
            return const Center(child: CircularProgressIndicator());
          } else if (state is ActivityLoaded) {
            return ListView.builder(
              itemCount: state.activities.length,
              itemBuilder: (context, index) {
                final activity = state.activities[index];
                return ListTile(
                  title: Text(activity.title),
                  subtitle: Text(activity.description),
                  trailing: Checkbox(
                    value: activity.isCompleted,
                    onChanged: (value) {
                      context.read<ActivityBloc>().add(UpdateActivityEvent(
                          activity: activity.copyWith(isCompleted: value!)));
                    },
                  ),
                );
              },
            );
          } else if (state is ActivityError) {
            return Center(child: Text(state.message));
          }
          return Container();
        },
      )),
      floatingActionButton: FloatingActionButton(
        onPressed: () {
          Navigator.push(
            context,
            MaterialPageRoute(builder: (context) => AddActivityForm()),
          );
        },
        child: Icon(Icons.add),
      ),
    );
  }
}
