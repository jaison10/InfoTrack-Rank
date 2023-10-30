import { Engine } from "./engine-model";

export interface RankHistory{
    id : string,
    url : string,
    positions : string,
    searchString : string,
    date : string,
    searchEngine : Engine
}