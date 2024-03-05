import 'package:akvt_raspisanie/DB/DB.dart';
import 'package:akvt_raspisanie/HelpersClasses/Lessons.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:provider/provider.dart';

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
                // Provider.of<GlobalGroup>(context,listen: false).ChangeGroup(item);
                Provider.of<Lessons>(context,listen: false).ChangeItem(items[index]);
                controller.closeView(item);

                },
            );
          });
          return _lastOptions;
        }
    );
  }
}
