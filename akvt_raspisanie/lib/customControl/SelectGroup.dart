import 'package:akvt_raspisanie/customControl/SearchBox.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import 'package:provider/provider.dart';
import '../HelpersClasses/Lessons.dart';

class SelectGroup extends StatefulWidget {
  const SelectGroup({super.key});

  @override
  State<SelectGroup> createState() => _SelectGroupState();
}

class _SelectGroupState extends State<SelectGroup> {

  @override
  Widget build(BuildContext context) {
    String group = Provider.of<Lessons>(context).GetGroup().item;
    return Padding(
        padding: EdgeInsets.fromLTRB(8, 0, 8, 8),
        child: Container(
          decoration: BoxDecoration(
              color: const Color.fromRGBO(243, 243, 243, 1),
              border: Border.all(style: BorderStyle.none),
              borderRadius: const BorderRadius.all(Radius.circular(16.0))),
          width: MediaQuery.of(context).size.width,
          child:  Row(
            children: [
              Center(
                  child: Padding(
                    padding: const EdgeInsets.fromLTRB(8, 0, 0, 0),
                    child: SvgPicture.asset('lib/res/icons/group-svgrepo-com.svg',
                        color: Colors.black,
                        width: 40.0,
                        height: 40.0),
                  )),
              Expanded(
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      const Padding(
                        padding: EdgeInsets.fromLTRB(8,8,8,0),
                        child: Text("Мое расписание",
                            overflow: TextOverflow.ellipsis,
                            style: TextStyle(
                                fontSize: 18.0,
                                fontFamily: 'Ubuntu',
                                color: Colors.black,
                                fontWeight: FontWeight.w500)),
                      ),
                      Padding(
                        padding: EdgeInsets.fromLTRB(8,8,8,8),
                        child: Text(group,
                            overflow: TextOverflow.ellipsis,
                            style: const TextStyle(
                                fontSize: 16.0,
                                fontFamily: 'Ubuntu',
                                color: Colors.black87,
                                fontWeight: FontWeight.w500)
                        ),
                      ),
                    ],
                  )
              ),
               Center(
                  child: Padding(
                    padding: const EdgeInsets.fromLTRB(8, 0, 8, 0),
                    child: SearchGroup(),
                  )),
            ],
          ),
        )
    );
  }
}
