import 'package:artcontainer_flutter/widgets/destinaltion.dart';
import 'package:artcontainer_flutter/widgets/hotel_carousel.dart';
import 'package:flutter/material.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';


class HomeScreen extends StatefulWidget {
  @override
  _HomeScreenState createState() => _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen> {
  int _selectedIndex = 0;
  int _currentTab = 0;

  List<IconData> _icons = [
    FontAwesomeIcons.plane,
    FontAwesomeIcons.bed,
    FontAwesomeIcons.walking,
    FontAwesomeIcons.biking
  ];

  Widget _buildingIcon(int index) {
    return GestureDetector(
      onTap: () {
        setState(() {
          _selectedIndex = index;
        });
        print(_selectedIndex);
      },
      child: Container(
        height: 60.0,
        width: 60.0,
        decoration: BoxDecoration(
            color: _selectedIndex == index
                ? Theme.of(context).accentColor
                : Color(0xFFE7EBEE),
            borderRadius: BorderRadius.circular(30.0)),
        child: Icon(
          _icons[index],
          size: 25.0,
          color: _selectedIndex == index
              ? Theme.of(context).primaryColor
              : Color(0xFFB4C1C4),
        ),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: ListView(
          padding: EdgeInsets.symmetric(vertical: 30.0),
          children: <Widget>[
            Padding(
              padding: const EdgeInsets.only(left: 20.0, right: 80.0),
              child: Text('What would you like to find?',
                  style:
                      TextStyle(fontSize: 30.0, fontWeight: FontWeight.bold)),
            ),
            SizedBox(
              height: 20.0,
            ),
            Row(
                mainAxisAlignment: MainAxisAlignment.spaceAround,
                children: _icons
                    .asMap()
                    .entries
                    .map(
                      (MapEntry map) => _buildingIcon(map.key),
                    )
                    .toList()),
                    SizedBox(height: 20.0,),
            DestinationCarousel(),
            SizedBox(height: 20.0,),
            HotelCarousel(),
          ],
        ),
      ),
      bottomNavigationBar: BottomNavigationBar(currentIndex: _currentTab,
      onTap: (int value) {
        setState(() {
          _currentTab = value;
        });
      },
      items: [BottomNavigationBarItem(
        icon: Icon(
          Icons.search,
          size:30.0,
        ),
        title: SizedBox.shrink(),
      ),
      BottomNavigationBarItem(
        icon: Icon(
          Icons.personal_video,
          size:30.0,
        ),
        title: SizedBox.shrink(),
      ),
      BottomNavigationBarItem(
        icon: CircleAvatar(
          radius: 15.0,
          backgroundImage: NetworkImage('https://scontent.fhph1-1.fna.fbcdn.net/v/t31.0-8/30073278_376255372853160_7949038723752619835_o.jpg?_nc_cat=110&_nc_oc=AQlvkigXhY1KVeua9xXCb4bVITpS1Ps-ygCpU3iN13UdesGiSHcYtzYrgBHPMrtjVaU&_nc_ht=scontent.fhph1-1.fna&oh=c00c93b359f9692f3e3982fd78724aa8&oe=5ED106E8'),
        ),
        title: SizedBox.shrink(),
      )],),
    );
  }
}
