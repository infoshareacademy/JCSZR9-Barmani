import { UnitModel } from "./unit.model";

export class IngredientModel{
    constructor(
        public ingredientId: number,
        public name: string,
        public quantity: number,
        public unit: UnitModel
    ){}
}