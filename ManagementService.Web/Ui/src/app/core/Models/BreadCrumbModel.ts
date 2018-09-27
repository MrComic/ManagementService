
export class BreadCrumbModel{
  constructor()
  {
    this.Title = "";
    this.BreadCrumb = [{ title: "", route: "" }]
  }

  Title:string;
  BreadCrumb:{title:string,route:string}[]
}
