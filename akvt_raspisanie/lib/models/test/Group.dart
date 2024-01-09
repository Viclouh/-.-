
import 'package:akvt_raspisanie/models/test/Direction.dart';

class Group{
   int id;
   Direction direction;
   Group? parentsGroup;

  Group(this.id, this.direction, this.parentsGroup);
}