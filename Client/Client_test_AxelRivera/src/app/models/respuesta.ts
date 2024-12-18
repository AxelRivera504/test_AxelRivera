export interface Respuesta<TEntity>{
  ok:boolean,
  codigo:string,
  mensaje:string,
  data:TEntity
}
