export interface User {
     //Interfaces in Typescript different form interfaces in C#, 
     //Can be used in TS to specify if somethins is a type of something
     username: string;
     token: string;
}

/* Assumes data is equal to number - unless set to string which then it assumes data is of type string.
     // Can also use colon(:) to set to types for data as number or string with (|) as the or
let data: number | string = "42";
     data = 10;


     // The interface sets the types of the values(color) in objects(car1) which inherit(:) the interface itself.
          //car1 has initial error because it doesnt have the value of topSpeed
          //car2 has initial error because the value fo color is supposed to be a string and not a number.
               //If change topSpeed to topSpeed?: number, this makes the value of topSpeed optional.
interface Car {
     color: string,
     model: string,
     topSpeed: number
}
const car1: Car = {
     color: 'blue',
     model: 'BMW'
}

const car2: Car = {
     color: 3,
     model: 'Mercedes',
     topSpeed: 100
}

//must set both params to number in order to multiply since they are the only types which can be multiplied.
     // Also if no return statement and just x*y, can set return type to void via :void instead of :number
const multiply = (x: number,y: number):number => {
     return x*y;
} 
*/