import 'package:akvt_raspisanie/models/test/Para.dart';

class Note{
    int id ;
    String name;
    bool isCompleted;
    DateTime dateTime;
    Para? para;
    String description;


   Note( this.id, this.isCompleted, this.dateTime, this.para, this.description, this.name);

}