import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-splits',
  templateUrl: './splits.component.html',
  styleUrls: ['./splits.component.css']
})
export class SplitsComponent implements OnInit {
  ngOnInit() {
    window.location.href = "https://localhost:3000/splits";
  }
}
