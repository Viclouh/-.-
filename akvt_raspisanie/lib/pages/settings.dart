import 'package:flutter/material.dart';
import '../customControl/CustomTitle.dart';
import '../customControl/SearchBox.dart';

class Settings extends StatefulWidget {
  const Settings({super.key});

  @override
  State<Settings> createState() => _SettingsState();
}

class _SettingsState extends State<Settings> {
  @override
  Widget build(BuildContext context) {
    return  const Scaffold(
      body: Column(
            crossAxisAlignment: CrossAxisAlignment.center,
            children: <Widget>[
              Padding(
                padding: EdgeInsets.fromLTRB(18, 30, 0, 0),
                child: CustomTitle(text: 'Настройки', isVisible: false),
              )
            ],
          )
    );
  }
}
