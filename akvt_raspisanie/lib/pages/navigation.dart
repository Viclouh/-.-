import 'package:akvt_raspisanie/pages/notes.dart';
import 'package:akvt_raspisanie/pages/settings.dart';
import 'package:flutter/material.dart';
import 'package:akvt_raspisanie/pages/home.dart';
import 'package:flutter_svg/flutter_svg.dart';

class Navigation extends StatefulWidget {
  const Navigation({super.key});

  @override
  State<Navigation> createState() => _NavigationState();
}

class _NavigationState extends State<Navigation> {
  int currentPageIndex = 1;

  @override
  Widget build(BuildContext context) {
    final ThemeData theme = Theme.of(context);
    return Scaffold(
      bottomNavigationBar: NavigationBarTheme(
        data: NavigationBarThemeData(
          labelTextStyle: MaterialStateProperty.all(
              const TextStyle(
              fontSize: 16.0,
              fontFamily: 'Ubuntu',
              color: Color.fromRGBO(51, 51, 51, 1),
              fontWeight: FontWeight.normal))
        ),
        child: NavigationBar(
          height: 60,
          labelBehavior: NavigationDestinationLabelBehavior.onlyShowSelected,
          onDestinationSelected: (int index) {
            setState(() {
              currentPageIndex = index;
            });
          },
          backgroundColor: Colors.transparent,
          indicatorColor: Color.fromRGBO(227, 228, 232, 1),
          selectedIndex: currentPageIndex,
          destinations:  <Widget>[
            NavigationDestination(
              icon: SvgPicture.asset('lib/res/icons/notes.svg'),
              label: 'Заметки',
            ),
            NavigationDestination(
              icon: SvgPicture.asset('lib/res/icons/calendar.svg'),
              label: 'Расписание',
            ),
            NavigationDestination(
              icon: SvgPicture.asset('lib/res/icons/settings.svg'),
              label: 'Настройки',
            ),
          ],
        ),
      ),
      body: <Widget>[
        Notes(),
        Home(),
        Settings(),


      ][currentPageIndex],
    );
  }
}

