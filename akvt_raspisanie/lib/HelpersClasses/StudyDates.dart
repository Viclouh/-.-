class StudyDates{
  static int GetWeekNum(DateTime date,int maxNumOfWeek){
    DateTime start = GetAcademicYearStart();
    start = GetNearest(start, 1);
    return (start.difference(date).inDays~/DateTime.daysPerWeek)~/maxNumOfWeek;
  }
  static DateTime GetNext( DateTime dateTime, int dayOfWeek){
    return dateTime.add(Duration(days: (dayOfWeek-dateTime.weekday) % DateTime.daysPerWeek));
  }
  static DateTime GetPrevious( DateTime dateTime, int dayOfWeek){
    return dateTime.add(Duration(days: -((dayOfWeek-dateTime.weekday) % DateTime.daysPerWeek)));
  }
  static GetNearest(DateTime dateTime, int dayOfWeek){
    DateTime next = GetNext(dateTime, dayOfWeek);
    DateTime previous = GetPrevious(dateTime, dayOfWeek);

    if (dateTime.difference(next).inDays.abs()>dateTime.difference(previous).inDays.abs()){
      return previous;
    }
    return next;
  }
  static DateTime GetAcademicYearStart(){
    DateTime firstDay = DateTime.utc(DateTime.now().year, 9, 1);
    if(firstDay.isAfter(DateTime.now())){

      return DateTime(firstDay.year-1,9,1);
    }
    return DateTime(firstDay.year,9,1);
  }
}