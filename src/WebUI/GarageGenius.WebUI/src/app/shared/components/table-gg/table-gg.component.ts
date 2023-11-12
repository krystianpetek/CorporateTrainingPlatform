import {Component, Input, OnInit} from '@angular/core';
import {MatTableDataSource, MatTableModule} from "@angular/material/table";
import {CommonModule} from "@angular/common";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-table-gg',
  templateUrl: './table-gg.component.html',
  styleUrl: './table-gg.component.scss',
  standalone : true,
  imports: [CommonModule, RouterLink, MatTableModule]
})
export class TableGgComponent<TElement> implements OnInit {
  @Input()
  tableColumns: Array<Column> = [];

  @Input()
  tableData: Array<TElement> = [];

  displayedColumns: Array<string> = [];
  dataSource: MatTableDataSource<TElement> = new MatTableDataSource();

  constructor() {}

  ngOnInit(): void {
    this.displayedColumns = this.tableColumns.map((c) => c.columnDef);
    this.dataSource = new MatTableDataSource(this.tableData);
  }
}

export interface Column {
  columnDef: string;
  header: string;
  cell: Function;
  isLink?: boolean;
  url?: string;
}

export interface Element {
  // position: number,
  // name: string,
  // weight: number,
  // symbol: string
  created: string
}
