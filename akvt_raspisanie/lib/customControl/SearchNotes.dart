import 'dart:ffi';

import 'package:akvt_raspisanie/DB/DB.dart';
import 'package:akvt_raspisanie/HelpersClasses/Lessons.dart';
import 'package:akvt_raspisanie/HelpersClasses/LevenshteinDistance.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:provider/provider.dart';
import 'dart:math';

class SearchBox extends StatefulWidget {
   SearchBox({super.key});

  @override
  State<SearchBox> createState() => _SearchBoxState();
}

class _SearchBoxState extends State<SearchBox> {
  String? _searchingWithQuery;
  late Iterable<Widget> _lastOptions = <Widget>[];

  String itemLowerCase(String item){
    return item.toLowerCase().replaceAll(' - ', '');
  }

  String selectLetters(String input) {
    String result = '';
    for (int i = 0; i < input.length; i++) {
      if (input[i].toUpperCase() != input[i].toLowerCase()) {
        result += input[i];
      }
    }
    return result;
  }


  @override
  Widget build(BuildContext context) {
    return SearchAnchor(
        builder: (BuildContext context, SearchController controller) {
          return SearchBar(
            controller: controller,
            onTap: () {
              controller.openView();
            },
            onChanged: (_) {
              controller.openView();
            },
          );
        },
        suggestionsBuilder: (BuildContext context,
            SearchController controller) async {
          List<Item> temp = [];
          List<Item> items = [];
          temp  = await AppDB.EditAllItems();

          List<int> lengths = temp.map((x)=>x.item.length).toList();

          int maximum =  lengths.reduce(max);

          _searchingWithQuery = controller.text;


//1
          // items = temp.where((element) =>
          // itemLowerCase(element.item).contains(_searchingWithQuery!.toLowerCase())||
          //     element.item.contains(_searchingWithQuery!)).toList();

//2
//           String s2 =  _searchingWithQuery!.toLowerCase();
//           List<Item> sorted = List.from(temp);
//           sorted.sort((x,y){
//             return LevenshteinDistance.levenshteinDistance(x.item.toLowerCase(),s2).compareTo(LevenshteinDistance.levenshteinDistance(y.item.toLowerCase(),s2));});
//           items = sorted;

//3
//           items = temp.where((element) =>
//           LevenshteinDistance.levenshteinDistance(itemLowerCase(element.item),_searchingWithQuery!.toLowerCase())<=4)
//               .toList();

//4
          int percent = (_searchingWithQuery!.length * 0.2).truncate();
          List<Item> filtered = [] ;
          temp.forEach((element) {
            for(int i  = 0 ; i <= element.item.length - _searchingWithQuery!.length; i++){
              String s1 = element.item.substring(i,i+_searchingWithQuery!.length);
              int distance =  LevenshteinDistance.levenshteinDistance(s1.toLowerCase(), _searchingWithQuery!.toLowerCase());
              if(distance<=percent) {
                filtered.add(element);
                return;
              }
            }
          });
          items = filtered;


          if (_searchingWithQuery != controller.text) {
            return _lastOptions;
          }



           _lastOptions = List<ListTile>.generate(items.length, (int index) {
            final String item = items[index].item;
            return ListTile(
              title: Text( item ),
              onTap: (){
                setState(() {
                  // Provider.of<GlobalGroup>(context,listen: false).ChangeGroup(item);
                  Provider.of<Lessons>(context,listen: false).ChangeItem(items[index]);
                  controller.closeView(item);
                  FocusScope.of(context).unfocus();

              });
                },
            );
          });
          return _lastOptions;
        }
    );
  }
}

class SearchGroup extends StatefulWidget {
  SearchGroup({super.key});

  @override
  State<SearchGroup> createState() => _SearchGroupState();
}

class _SearchGroupState extends State<SearchGroup> {
  String? _searchingWithQuery;
  late Iterable<Widget> _lastOptions = <Widget>[];

  String itemLowerCase(String item){
    return item.toLowerCase().replaceAll(' - ', '');
  }

  @override
  Widget build(BuildContext context) {
    return SearchAnchor(
        builder: (BuildContext context, SearchController controller) {
          return IconButton(
            icon: Icon(Icons.arrow_forward_ios),
            onPressed: (){controller.openView();
              },
          );
        },
        suggestionsBuilder: (BuildContext context,
            SearchController controller) async {
          List<Item> temp = [];
          List<Item> items = [];
          temp  = await AppDB.EditGroupsAndTeachers();

          _searchingWithQuery = controller.text;

          items = temp.where((element) =>
          itemLowerCase(element.item).contains(_searchingWithQuery!.toLowerCase())||
              element.item.contains(_searchingWithQuery!)).toList();

          if (_searchingWithQuery != controller.text) {
            return _lastOptions;
          }



          _lastOptions = List<ListTile>.generate(items.length, (int index) {
            final String item = items[index].item;
            return ListTile(
              title: Text( item ),
              onTap: (){
                setState(() {
                  // Provider.of<GlobalGroup>(context,listen: false).ChangeGroup(item);

                  Provider.of<Lessons>(context,listen: false).ChangeGroup(items[index]);
                  controller.closeView(item);
                  FocusScope.of(context).unfocus();

                });
              },
            );
          });
          return _lastOptions;
        }
    );
  }
}
