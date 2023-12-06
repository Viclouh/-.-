import 'package:flutter/material.dart';
import 'package:akvt_raspisanie/customControl/SearchBox.dart';
import 'package:akvt_raspisanie/customControl/CustomTitle.dart';
import 'package:akvt_raspisanie/customControl/Calendar.dart';


class Home extends StatefulWidget {
  const Home({super.key});

  @override
  State<Home> createState() => _HomeState();
}

class _HomeState extends State<Home> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body:  Column(
        children: <Widget>[
          const SearchBox(),
          CustomTitle(text:'Группа-11'),
          TableBasicsExample(),
        ],
      ),
    );
  }
}
