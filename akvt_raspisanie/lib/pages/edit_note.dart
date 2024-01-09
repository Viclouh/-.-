
import 'package:flutter/material.dart';
import 'package:flutter_datetime_picker_plus/flutter_datetime_picker_plus.dart';
import 'package:flutter_svg/svg.dart';
import 'package:intl/intl.dart';
import '../DB/Notes.dart';
import '../customControl/CustomTitle.dart';

class EditNote extends StatefulWidget {
  // TODO note;

  EditNote({super.key, /*required this.note*/});

  @override
  State<EditNote> createState() => _EditNoteState(/*this.note*/);
}

class _EditNoteState extends State<EditNote> {

  // _EditNoteState(this.note);
  // TODO note;
  final _formKey = GlobalKey<FormState>();
  DateTime _selectedDay = DateTime.now();
  static final DateTime _firstDay = getFirstDay();
  final DateTime _lastDay = DateTime.utc(_firstDay.year+1, _firstDay.month, _firstDay.day);
  String _description = "";
  String _name = '';

  Future<void> EditTODO() async{
    final database = AppDatabase();
    database.into(database.tODOs).insert(TODOsCompanion.insert(
        name: _name,
        isCompleted: false,
        datetime: _selectedDay,
        description: _description)
    );
  }
  _changeName(String text){
    setState(() {
      _name = text;
    });
  }
  _changeDescription(String text){
    setState(() {
      _description = text;
    });
  }
  static DateTime getFirstDay(){
    DateTime firstDay = DateTime.utc(DateTime.now().year, 9, 1);
    if(firstDay.isAfter(DateTime.now())){

      return DateTime(firstDay.year-1,9,1);
    }
    return DateTime(firstDay.year,9,1);
  }

  @override
  Widget build(BuildContext context) {
    return  Scaffold(
      // appBar:AppBar(
      //   title: CustomTitle(text: 'Новая задача', isVisible: false)
      //   backgroundColor: ,
      // ),
      body: Form(
        key: _formKey,
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            const Align(
                alignment: Alignment.topLeft,
                child: Padding(
                  padding: EdgeInsets.fromLTRB(18, 12, 0, 0),
                  child: CustomTitle(text: 'Новая задача', isVisible: false),
                )
            ),
            Padding(
              padding: const EdgeInsets.fromLTRB(8, 8, 8, 0),
              child: Container(
                  decoration: BoxDecoration(
                      color:
                      const Color.fromRGBO(227, 228, 232, 1),
                      border: Border.all(style: BorderStyle.none),
                      borderRadius:  const BorderRadius.all(
                          Radius.circular(15.0))),
                  width: MediaQuery.of(context).size.width,
                  child: Row(
                    children: [
                      Center(
                          child: Padding(
                            padding:  const EdgeInsets.fromLTRB(8, 0, 8, 0),
                            child: SvgPicture.asset(
                                'lib/res/icons/input-field.svg',
                                color:  const Color.fromRGBO(51, 51, 51, 1),
                                width: 24.0,
                                height: 24.0),
                          )
                      ),
                      Expanded(
                        child: TextFormField(
                            validator: (value) {
                              if (value == null || value.isEmpty) {
                                return 'Введите название';
                              }
                              return null;
                            },
                            initialValue: _name,
                            onChanged: _changeName,
                            style: const TextStyle(
                                fontSize: 18.0,
                                fontFamily: 'Ubuntu',
                                color: Colors.black
                            ),
                            decoration: const InputDecoration(
                              hintText: "Название",
                              focusedBorder: InputBorder.none,
                              enabledBorder: InputBorder.none,
                            )
                        ),
                      )
                    ],
                  )
              ),
            ),
            Padding(
              padding: const EdgeInsets.fromLTRB(8, 8, 8, 0),
              child: Container(
                  decoration: BoxDecoration(
                      color:
                      const Color.fromRGBO(227, 228, 232, 1),
                      border: Border.all(style: BorderStyle.none),
                      borderRadius:  const BorderRadius.all(
                          Radius.circular(15.0))),
                  height: 46,
                  width: MediaQuery.of(context).size.width,
                  child: Row(
                    children: [
                      Center(
                          child: Padding(
                            padding:  const EdgeInsets.fromLTRB(8, 0, 0, 0),
                            child: SvgPicture.asset(
                                'lib/res/icons/calendar.svg',
                                color:  const Color.fromRGBO(51, 51, 51, 1),
                                width: 24.0,
                                height: 24.0),
                          )
                      ),
                      Expanded(
                          child: Container(
                            height: double.infinity,
                            child: TextButton(
                                onPressed: () {
                                  DatePicker.showDateTimePicker(context,
                                      showTitleActions: true,
                                      currentTime: DateTime.now(),
                                      locale: LocaleType.ru,
                                      minTime: _firstDay,
                                      maxTime: DateTime.utc(_firstDay.year+1, _firstDay.month, _firstDay.day),
                                      onChanged: (date) {
                                        print('change $date');
                                      },
                                      onConfirm: (date) {
                                        print('confirm $date');
                                        setState(() {
                                          _selectedDay = date;
                                        }
                                        );
                                      }
                                  );
                                },
                                style: const ButtonStyle(
                                    alignment: Alignment.centerLeft
                                ),
                                child:  Text(
                                  '${DateFormat.yMMMMEEEEd('ru').format(_selectedDay)}, ${DateFormat.Hm().format(_selectedDay)}',
                                  style: const TextStyle(
                                      fontSize: 18.0,
                                      fontFamily: 'sdf',
                                      color: Colors.black
                                  ),
                                )
                            ),
                          )
                      ),
                    ],
                  )
              ),
            ),
            Padding(
              padding: const EdgeInsets.fromLTRB(8, 8, 8, 0),
              child: Container(
                  decoration: BoxDecoration(
                      color:
                      const Color.fromRGBO(227, 228, 232, 1),
                      border: Border.all(style: BorderStyle.none),
                      borderRadius:  const BorderRadius.all(
                          Radius.circular(15.0))),
                  width: MediaQuery.of(context).size.width,
                  child: Row(
                    crossAxisAlignment:CrossAxisAlignment.start,
                    children: [
                      Padding(
                        padding:  const EdgeInsets.fromLTRB(8, 10, 0, 0),
                        child: SvgPicture.asset(
                            'lib/res/icons/edit-2.svg',
                            color:  const Color.fromRGBO(51, 51, 51, 1),
                            width: 24.0,
                            height: 24.0),
                      ),
                      Expanded(
                        child:Center(
                          child: Padding(
                            padding: const EdgeInsets.fromLTRB(8, 0, 0, 8),
                            child: TextFormField(
                                initialValue: _description,
                                onChanged: _changeDescription,
                                validator: (value) {
                                  if (value == null || value.isEmpty) {
                                    return 'Введите описание';
                                  }
                                  return null;
                                },
                                keyboardType: TextInputType.multiline,
                                minLines: 3,
                                maxLines: 10,
                                style: const TextStyle(
                                    fontSize: 18.0,
                                    fontFamily: 'Ubuntu',
                                    color: Colors.black
                                ),
                                decoration: const InputDecoration(
                                  hintText: "Описание",
                                  focusedBorder: InputBorder.none,
                                  enabledBorder: InputBorder.none,
                                )
                            ),
                          ),
                        ),
                      )
                    ],
                  )
              ),
            ),
            Padding(
                padding: const EdgeInsets.fromLTRB(8, 16, 8, 8),
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                  children: [
                    IconButton(
                        onPressed: (){
                          Navigator.pushReplacementNamed(context, '/navigation');
                        },
                        icon:Icon(Icons.arrow_back_outlined)
                    ),
                    IconButton(
                        onPressed: (){
                          if (_formKey.currentState!.validate()){
                            EditTODO().then((value) => setState(() {}));
                            Navigator.pushReplacementNamed(context, '/navigation');
                          }
                        },
                        icon:Icon(Icons.check)
                    ),
                  ],
                )
            ),
          ],
        ),
      )
    );
  }
}
