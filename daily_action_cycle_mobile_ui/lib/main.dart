// import 'package:daily_action_cycle_mobile_ui/presentation/pages/activity_page.dart';
// import 'package:flutter/material.dart';
// import 'package:date_picker_timeline/date_picker_timeline.dart';

// void main() {
//   runApp(const MyApp());
// }

// class MyApp extends StatelessWidget {
//   const MyApp({super.key});

//   @override
//   Widget build(BuildContext context) {
//     return MaterialApp(
//       title: 'Flutter Demo',
//       theme: ThemeData(
//         colorScheme: ColorScheme.fromSeed(seedColor: Colors.lightBlue),
//         useMaterial3: true,
//       ),
//       darkTheme: ThemeData.dark(),
//       home: const BottomNavigationBarExample(
//         title: "DailyActionCycle",
//       ),
//     );
//   }
// }

// class MyHomePage extends StatefulWidget {
//   const MyHomePage({super.key, required this.title});
//   final String title;

//   @override
//   State<MyHomePage> createState() => _MyHomePageState();
// }

// class _MyHomePageState extends State<MyHomePage> {
//   int _counter = 0;

//   void _incrementCounter() {
//     setState(() {
//       _counter++;
//     });
//   }

//   @override
//   Widget build(BuildContext context) {
//     return Scaffold(
//       appBar: AppBar(
//         backgroundColor: Theme.of(context).colorScheme.inversePrimary,
//         title: Text(widget.title),
//       ),
//       body: Center(
//         child: Column(
//           mainAxisAlignment: MainAxisAlignment.center,
//           children: <Widget>[
//             const Text(
//               'You have pushed the button this many times:',
//             ),
//             Text(
//               '$_counter',
//               style: Theme.of(context).textTheme.headlineMedium,
//             ),
//           ],
//         ),
//       ),
//       floatingActionButton: FloatingActionButton(
//         onPressed: _incrementCounter,
//         tooltip: 'Increment',
//         child: const Icon(Icons.add),
//       ),
//     );
//   }
// }

// class BottomNavigationBarExample extends StatefulWidget {
//   const BottomNavigationBarExample({super.key, required this.title});

//   final String title;

//   @override
//   State<StatefulWidget> createState() {
//     return _BottomNavigationBarExampleState();
//   }
// }

// class _BottomNavigationBarExampleState
//     extends State<BottomNavigationBarExample> {
//   int _selectedBarIndex = 0;

//   static const TextStyle optionStyle =
//       TextStyle(fontSize: 30, fontWeight: FontWeight.bold);

//   static DateTime _selectedValue = DateTime.now();

//   static final List<Widget> _widgetOptions = <Widget>[
//     Column(
//       mainAxisAlignment: MainAxisAlignment.center,
//       children: [
//         Text(
//           'Selected date: $_selectedValue',
//           style: optionStyle,
//         ),
//         DatePicker(
//           height: 100,
//           DateTime.now(),
//           initialSelectedDate: DateTime.now(),
//           selectionColor: Colors.white,
//           selectedTextColor: Colors.black,
//           deactivatedColor: Colors.grey,
//           daysCount: 7,
//           dayTextStyle: const TextStyle(color: Colors.white),
//           onDateChange: (date) {
//             // New date selected
//             _selectedValue = date;
//           },
//         ),
//         Text(
//           'Selected date: $_selectedValue',
//           style: optionStyle,
//         ),
//       ],
//     ),
//     ActivityPage(),
//     const Text(
//       'Index 2: School',
//       style: optionStyle,
//     ),
//   ];

//   void _onItemTapped(int index) {
//     setState(() {
//       _selectedBarIndex = index;
//     });
//   }

//   @override
//   Widget build(BuildContext context) {
//     return Scaffold(
//       appBar: AppBar(
//         title: Text(widget.title),
//       ),
//       body: Center(
//         child: _widgetOptions.elementAt(_selectedBarIndex),
//       ),
//       bottomNavigationBar: BottomNavigationBar(
//         items: const <BottomNavigationBarItem>[
//           BottomNavigationBarItem(
//             icon: Icon(Icons.calendar_today),
//             label: 'Days',
//           ),
//           BottomNavigationBarItem(
//             icon: Icon(Icons.cyclone),
//             label: 'Action templates',
//           ),
//           BottomNavigationBarItem(
//             icon: Icon(Icons.school),
//             label: 'School',
//           ),
//         ],
//         currentIndex: _selectedBarIndex,
//         selectedItemColor: Colors.amber[800],
//         onTap: _onItemTapped,
//       ),
//     );
//   }
// }

/////////////// PROBLEM ZE STATYCZNA ZMIENNA

// import 'package:daily_action_cycle_mobile_ui/presentation/pages/activity_page.dart';
// import 'package:flutter/material.dart';
// import 'package:flutter_bloc/flutter_bloc.dart';
// import 'package:date_picker_timeline/date_picker_timeline.dart';
// import 'injection_container.dart' as di;
// import 'presentation/blocs/activity_bloc.dart';

// void main() async {
//   WidgetsFlutterBinding.ensureInitialized();
//   await di.init();
//   runApp(const MyApp());
// }

// class MyApp extends StatelessWidget {
//   const MyApp({super.key});

//   @override
//   Widget build(BuildContext context) {
//     return MultiBlocProvider(
//       providers: [
//         BlocProvider(
//           create: (_) => di.sl<ActivityBloc>()..add(LoadActivities()),
//         ),
//       ],
//       child: MaterialApp(
//         title: 'Flutter Demo',
//         theme: ThemeData(
//           colorScheme: ColorScheme.fromSeed(seedColor: Colors.lightBlue),
//           useMaterial3: true,
//         ),
//         darkTheme: ThemeData.dark(),
//         home: const BottomNavigationBarExample(
//           title: "DailyActionCycle",
//         ),
//       ),
//     );
//   }
// }

// class BottomNavigationBarExample extends StatefulWidget {
//   const BottomNavigationBarExample({super.key, required this.title});

//   final String title;

//   @override
//   State<StatefulWidget> createState() {
//     return _BottomNavigationBarExampleState();
//   }
// }

// class _BottomNavigationBarExampleState
//     extends State<BottomNavigationBarExample> {
//   int _selectedBarIndex = 0;
//   DateTime _selectedValue = DateTime.now();

//   static const TextStyle optionStyle =
//       TextStyle(fontSize: 30, fontWeight: FontWeight.bold);

//   static List<Widget> _widgetOptions(DateTime selectedValue) => <Widget>[
//         Column(
//           mainAxisAlignment: MainAxisAlignment.center,
//           children: [
//             Text(
//               'Selected date: $selectedValue',
//               style: optionStyle,
//             ),
//             DatePicker(
//               height: 100,
//               DateTime.now(),
//               initialSelectedDate: DateTime.now(),
//               selectionColor: Colors.white,
//               selectedTextColor: Colors.black,
//               deactivatedColor: Colors.grey,
//               daysCount: 7,
//               dayTextStyle: const TextStyle(color: Colors.white),
//               onDateChange: (date) {
//                 // New date selected
//                 setState(() {
//                   _selectedValue = date;
//                 });
//               },
//             ),
//             Text(
//               'Selected date: $selectedValue',
//               style: optionStyle,
//             ),
//           ],
//         ),
//         ActivityPage(),
//         const Text(
//           'Index 2: School',
//           style: optionStyle,
//         ),
//       ];

//   void _onItemTapped(int index) {
//     setState(() {
//       _selectedBarIndex = index;
//     });
//   }

//   @override
//   Widget build(BuildContext context) {
//     return Scaffold(
//       appBar: AppBar(
//         title: Text(widget.title),
//       ),
//       body: Center(
//         child: _widgetOptions(_selectedValue).elementAt(_selectedBarIndex),
//       ),
//       bottomNavigationBar: BottomNavigationBar(
//         items: const <BottomNavigationBarItem>[
//           BottomNavigationBarItem(
//             icon: Icon(Icons.calendar_today),
//             label: 'Days',
//           ),
//           BottomNavigationBarItem(
//             icon: Icon(Icons.cyclone),
//             label: 'Action templates',
//           ),
//           BottomNavigationBarItem(
//             icon: Icon(Icons.school),
//             label: 'School',
//           ),
//         ],
//         currentIndex: _selectedBarIndex,
//         selectedItemColor: Colors.amber[800],
//         onTap: _onItemTapped,
//       ),
//     );
//   }
// }

import 'dart:io';

import 'package:daily_action_cycle_mobile_ui/presentation/pages/activity_page.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:date_picker_timeline/date_picker_timeline.dart';
import 'injection_container.dart' as di;
import 'presentation/blocs/activity_bloc.dart';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  await di.init();
  HttpOverrides.global = MyHttpOverrides();
  runApp(const MyApp());
}

class MyHttpOverrides extends HttpOverrides {
  @override
  HttpClient createHttpClient(SecurityContext? context) {
    return super.createHttpClient(context)
      ..badCertificateCallback = (X509Certificate cert, String host, int port) => true;
  }
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MultiBlocProvider(
      providers: [
        BlocProvider(
          create: (_) => di.sl<ActivityBloc>()..add(LoadActivities()),
        ),
      ],
      child: MaterialApp(
        title: 'Flutter Demo',
        theme: ThemeData(
          colorScheme: ColorScheme.fromSeed(seedColor: Colors.lightBlue),
          useMaterial3: true,
        ),
        darkTheme: ThemeData.dark(),
        home: const BottomNavigationBarExample(
          title: "DailyActionCycle",
        ),
      ),
    );
  }
}

class BottomNavigationBarExample extends StatefulWidget {
  const BottomNavigationBarExample({super.key, required this.title});

  final String title;

  @override
  State<StatefulWidget> createState() {
    return _BottomNavigationBarExampleState();
  }
}

class _BottomNavigationBarExampleState
    extends State<BottomNavigationBarExample> {
  int _selectedBarIndex = 0;
  DateTime _selectedValue = DateTime.now();

  static const TextStyle optionStyle =
      TextStyle(fontSize: 30, fontWeight: FontWeight.bold);

  List<Widget> _widgetOptions() => <Widget>[
        Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Text(
              'Selected date: $_selectedValue',
              style: optionStyle,
            ),
            DatePicker(
              height: 100,
              DateTime.now(),
              initialSelectedDate: DateTime.now(),
              selectionColor: Colors.white,
              selectedTextColor: Colors.black,
              deactivatedColor: Colors.grey,
              daysCount: 7,
              dayTextStyle: const TextStyle(color: Colors.white),
              onDateChange: (date) {
                // New date selected
                setState(() {
                  _selectedValue = date;
                });
              },
            ),
            Text(
              'Selected date: $_selectedValue',
              style: optionStyle,
            ),
          ],
        ),
        ActivityPage(),
        const Text(
          'Index 2: School',
          style: optionStyle,
        ),
      ];

  void _onItemTapped(int index) {
    setState(() {
      _selectedBarIndex = index;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(widget.title),
      ),
      body: Center(
        child: _widgetOptions().elementAt(_selectedBarIndex),
      ),
      bottomNavigationBar: BottomNavigationBar(
        items: const <BottomNavigationBarItem>[
          BottomNavigationBarItem(
            icon: Icon(Icons.calendar_today),
            label: 'Days',
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.cyclone),
            label: 'Action templates',
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.school),
            label: 'School',
          ),
        ],
        currentIndex: _selectedBarIndex,
        selectedItemColor: Colors.amber[800],
        onTap: _onItemTapped,
      ),
    );
  }
}
