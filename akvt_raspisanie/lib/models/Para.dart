import 'package:akvt_raspisanie/models/Subject.dart';
import 'package:akvt_raspisanie/models/Cabinet.dart';
import 'package:akvt_raspisanie/models/Group.dart';
import 'package:akvt_raspisanie/models/Teacher.dart';

class Para {
  int id;
  int numPara;
  Subject subject;
  Cabinet cabinet;
  Group group;
  bool isDistant = false;
  int numWeek;
  Teacher teacher;
  DateTime date;
  DateTime start;
  DateTime end;

  Para(
      this.id,
      this.numPara,
      this.subject,
      this.cabinet,
      this.group,
      this.isDistant,
      this.numWeek,
      this.teacher,
      this.date,
      this.start,
      this.end);
}
