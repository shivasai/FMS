import { Component,ViewChild, OnInit } from '@angular/core';
import { jqxGridComponent } from 'jqwidgets-ng/jqxgrid';
import { environment } from '@environments/environment';
@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.less']
})
export class FeedbackComponent implements OnInit {
  //@ViewChild('feedbackgrid', { static: false }) feedbackgrid: jqxGridComponent;
  constructor() { }

  ngOnInit(): void {
  }
  @ViewChild('jqGrid', { static: false }) jqGrid: jqxGridComponent; 
  //data = '[{ "CompanyName": "Alfreds Futterkiste", "ContactName": "Maria Anders", "ContactTitle": "Sales Representative", "Address": "Obere Str. 57", "City": "Berlin", "Country": "Germany" }, { "CompanyName": "Ana Trujillo Emparedados y helados", "ContactName": "Ana Trujillo", "ContactTitle": "Owner", "Address": "Avda. de la Constitucin 2222", "City": "Mxico D.F.", "Country": "Mexico" }, { "CompanyName": "Antonio Moreno Taquera", "ContactName": "Antonio Moreno", "ContactTitle": "Owner", "Address": "Mataderos 2312", "City": "Mxico D.F.", "Country": "Mexico" }, { "CompanyName": "Around the Horn", "ContactName": "Thomas Hardy", "ContactTitle": "Sales Representative", "Address": "120 Hanover Sq.", "City": "London", "Country": "UK" }, { "CompanyName": "Berglunds snabbkp", "ContactName": "Christina Berglund", "ContactTitle": "Order Administrator", "Address": "Berguvsvgen 8", "City": "Lule", "Country": "Sweden" }, { "CompanyName": "Blauer See Delikatessen", "ContactName": "Hanna Moos", "ContactTitle": "Sales Representative", "Address": "Forsterstr. 57", "City": "Mannheim", "Country": "Germany" }, { "CompanyName": "Blondesddsl pre et fils", "ContactName": "Frdrique Citeaux", "ContactTitle": "Marketing Manager", "Address": "24, place Klber", "City": "Strasbourg", "Country": "France" }, { "CompanyName": "Blido Comidas preparadas", "ContactName": "Martn Sommer", "ContactTitle": "Owner", "Address": "C\/ Araquil, 67", "City": "Madrid", "Country": "Spain" }, { "CompanyName": "Bon app\'", "ContactName": "Laurence Lebihan", "ContactTitle": "Owner", "Address": "12, rue des Bouchers", "City": "Marseille", "Country": "France" }, { "CompanyName": "Bottom-Dollar Markets", "ContactName": "Elizabeth Lincoln", "ContactTitle": "Accounting Manager", "Address": "23 Tsawassen Blvd.", "City": "Tsawassen", "Country": "Canada" }, { "CompanyName": "B\'s Beverages", "ContactName": "Victoria Ashworth", "ContactTitle": "Sales Representative", "Address": "Fauntleroy Circus", "City": "London", "Country": "UK" }, { "CompanyName": "Cactus Comidas para llevar", "ContactName": "Patricio Simpson", "ContactTitle": "Sales Agent", "Address": "Cerrito 333", "City": "Buenos Aires", "Country": "Argentina" }, { "CompanyName": "Centro comercial Moctezuma", "ContactName": "Francisco Chang", "ContactTitle": "Marketing Manager", "Address": "Sierras de Granada 9993", "City": "Mxico D.F.", "Country": "Mexico" }, { "CompanyName": "Chop-suey Chinese", "ContactName": "Yang Wang", "ContactTitle": "Owner", "Address": "Hauptstr. 29", "City": "Bern", "Country": "Switzerland" }, { "CompanyName": "Comrcio Mineiro", "ContactName": "Pedro Afonso", "ContactTitle": "Sales Associate", "Address": "Av. dos Lusadas, 23", "City": "Sao Paulo", "Country": "Brazil" }, { "CompanyName": "Consolidated Holdings", "ContactName": "Elizabeth Brown", "ContactTitle": "Sales Representative", "Address": "Berkeley Gardens 12 Brewery", "City": "London", "Country": "UK" }, { "CompanyName": "Drachenblut Delikatessen", "ContactName": "Sven Ottlieb", "ContactTitle": "Order Administrator", "Address": "Walserweg 21", "City": "Aachen", "Country": "Germany" }, { "CompanyName": "Du monde entier", "ContactName": "Janine Labrune", "ContactTitle": "Owner", "Address": "67, rue des Cinquante Otages", "City": "Nantes", "Country": "France" }, { "CompanyName": "Eastern Connection", "ContactName": "Ann Devon", "ContactTitle": "Sales Agent", "Address": "35 King George", "City": "London", "Country": "UK" }, { "CompanyName": "Ernst Handel", "ContactName": "Roland Mendel", "ContactTitle": "Sales Manager", "Address": "Kirchgasse 6", "City": "Graz", "Country": "Austria"}]';
  //data = [{"id":2,"eventId":"EVNT00047261","month":"DEC","baseLocation":"Singapore","beneficiaryName":"KWONG WAI SHIU HOSPITAL","venueAddress":"705, Serangoon Road, Singapore, Singapore, Singapore-328127","councilName":"Outreach Singapore","project":"Donation or Distribution","category":"Essentials or relief","eventName":"Bags of Joy Distribution","eventDescription":"Thank you for all your donations of food items to make this a good Xmas for everyone! Come be a Santa and distribute these Bags of Joy to elderly low income residents in Central Singapore and feel the joy of giving!Friends and family welcome!","eventDate":"2018-01-12T00:00:00","totalNoOfVolunteers":21,"totalVolunteerHours":88,"totalTravelHours":42,"overallVolunteeringHours":130,"livesImpacted":800,"activityType":"1","status":"Approved"},{"id":3,"eventId":"EVNT00046103","month":"DEC","baseLocation":"Chennai","beneficiaryName":"Kamarajar Illam,Tambaram","venueAddress":"Gandhi Rd, Ranganathapuram, Tambaram, Tambaram, , , India-600045","councilName":"Chennai BFS Outreach","project":"Be a Teacher","category":"Multiple Subjects","eventName":"Be a Teacher @ Kamarajar Illam","eventDescription":"Teach various subjects to the students in Kamarajar Illam","eventDate":"2018-01-12T00:00:00","totalNoOfVolunteers":14,"totalVolunteerHours":28,"totalTravelHours":14,"overallVolunteeringHours":42,"livesImpacted":30,"activityType":"3","status":"Approved"},{"id":4,"eventId":"EVNT00046385","month":"DEC","baseLocation":"United Kingdom","beneficiaryName":"St. Edward’s CE Voluntary Aided Primary School","venueAddress":"Havering Drive, Havering Drive, , , Romford-RM1 4BT","councilName":"Outreach UK","project":"Community Program","category":"Other Community Programs","eventName":"1st Dec PM-Christmas fair to save a school swimming pool","eventDescription":"The school is hosting a Christmas Fayre to raise money to refurbish their swimming pool that is used by the children in the local community. Volunteers needed to set up, run and pack up the stalls. Can make it a fun day with your family.","eventDate":"2018-01-12T00:00:00","totalNoOfVolunteers":4,"totalVolunteerHours":24,"totalTravelHours":0,"overallVolunteeringHours":24,"livesImpacted":0,"activityType":"3","status":"Approved"},{"id":5,"eventId":"EVNT00046530","month":"DEC","baseLocation":"Pune","beneficiaryName":"Gurukulam","venueAddress":"Krantiveer Chapekar Smarak Samitee, Opposite Ram Mandir, Chinchwad Gaon, Pune, Maharashtra, India-411033","councilName":"Pune QEA LBG Outreach","project":"Be a Teacher","category":"Other Subject","eventName":"Improve in Co-ordination","eventDescription":"There is a lack of co-ordination in between the students and no friendly relation to each other.  Through good talk, learning game we will improve co-ordination in between the students.","eventDate":"2018-01-12T00:00:00","totalNoOfVolunteers":2,"totalVolunteerHours":5,"totalTravelHours":0,"overallVolunteeringHours":5,"livesImpacted":20,"activityType":"4","status":"Approved"},{"id":6,"eventId":"EVNT00046531","month":"DEC","baseLocation":"Pune","beneficiaryName":"Gurukulam","venueAddress":"Krantiveer Chapekar Smarak Samitee, Opposite Ram Mandir, Chinchwad Gaon, Pune, Maharashtra, India-411033","councilName":"Pune QEA LBG Outreach","project":"Be a Teacher","category":"Other Subject","eventName":"Improve in Co-ordination","eventDescription":"There is a lack of co-ordination in between the students and no friendly relation to each other.  Through good talk, learning game we will improve co-ordination in between the students.","eventDate":"2018-08-12T00:00:00","totalNoOfVolunteers":2,"totalVolunteerHours":4,"totalTravelHours":0,"overallVolunteeringHours":4,"livesImpacted":2,"activityType":"4","status":"Approved"},{"id":7,"eventId":"EVNT00046588","month":"DEC","baseLocation":"Chennai","beneficiaryName":"ADW Primary school chitlapakkam","venueAddress":"Chitlapakkam, , , India-600064","councilName":"Chennai BPS Outreach","project":"Be a Teacher","category":"English","eventName":"BAT","eventDescription":"BAT","eventDate":"2018-03-12T00:00:00","totalNoOfVolunteers":2,"totalVolunteerHours":4,"totalTravelHours":0,"overallVolunteeringHours":4,"livesImpacted":2,"activityType":"4","status":"Approved"},{"id":8,"eventId":"EVNT00046611","month":"DEC","baseLocation":"Coimbatore","beneficiaryName":"Panchayat Union Primary School, Keeranatham Puthupalayam","venueAddress":"Keeranatham Puthupalayam, Coimbatore, Tamil nadu, India-641035","councilName":"Coimbatore Outreach","project":"Be a Teacher","category":"Multiple Subjects","eventName":"TEACHING","eventDescription":"Teaches English Grammar","eventDate":"2018-01-12T00:00:00","totalNoOfVolunteers":5,"totalVolunteerHours":10,"totalTravelHours":2.5,"overallVolunteeringHours":12.5,"livesImpacted":22,"activityType":"3","status":"Approved"},{"id":9,"eventId":"EVNT00047114","month":"DEC","baseLocation":"Chennai","beneficiaryName":"Panchayat Union Primary School, Amman Nagar","venueAddress":"Amman Nagar, Trisulam, Chennai, Tamil Nadu, India-600043","councilName":"Chennai RCG Outreach","project":"Be a Teacher","category":"English","eventName":"Be a Teacher","eventDescription":"To teach \"english\" (mainly spellings) to \"D\" Category students of 4th std students (A and B section).","eventDate":"2018-04-12T00:00:00","totalNoOfVolunteers":6,"totalVolunteerHours":0.6,"totalTravelHours":9,"overallVolunteeringHours":9.6,"livesImpacted":1,"activityType":"5","status":"Approved"}]
    source: any =
    {          
      datatype: 'json',
      datafields: [              
        { name: 'id', type: 'number' },   
          { name: 'question', type: 'string' },
          { name: 'optionsCount', type: 'string' },  
          { name: 'participantType', type: 'string' }         
      ],
      //localdata: this.data
      url: `${environment.apiUrl}/Feedback`
    };
    getWidth() : any {
      if (document.body.offsetWidth < 850) {
        return '100%';
      }
      
      return '100%';
    }
      dataAdapter: any = new jqx.dataAdapter(this.source);
      columns: any[] =
      [            
          { text: 'Question', datafield: 'question', width: '25%' },
          { text: 'Total Answers', datafield: 'optionsCount', width: '25%' }, 
          { text: 'Feedback Type', datafield: 'participantType', width: '25%' },   
          { text: 'Action',columntype: 'template',datafield:'id', type: 'bool', width:'25%',          
                        cellsrenderer:  function (row,column,value){                          
                          return `<a class="mt-3 ml-3" href="/feedbackdetails?qId=`+value+`">View</a>`
                          
                    }},          
      ];
    excelBtnOnClick() {
        this.jqGrid.exportdata('xls', 'Pmo_Users');
    };
    clearFiltering(): void {
      this.jqGrid.clearfilters();
    }; 
   
}
