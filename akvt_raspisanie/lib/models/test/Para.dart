import 'package:akvt_raspisanie/models/test/Subject.dart';
import 'package:akvt_raspisanie/models/test/Cabinet.dart';
import 'package:akvt_raspisanie/models/Teacher.dart';
import 'package:akvt_raspisanie/models/test/Group.dart';

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
