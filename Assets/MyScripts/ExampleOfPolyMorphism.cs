using UnityEngine;

namespace AH2694
{
    class ExampleOfPolyMorphism
    {
        
    }
    class Weapon//base class
    {
        public int weaponDamage;
        public int weaponRange;
        public Weapon() { Debug.Log("Weapon constructed"); }
        public virtual void Attack()// Virtual method to allow overriding
        {
            Debug.Log("Weapon attacks");
        }
    }
    class Sword : Weapon//derived class
    {
        public Sword() { Debug.Log("Sword constructed"); weaponDamage = 15; weaponRange = 2; }
    public override void Attack()// Overriding the base class method
        {
            Debug.Log("Sword swings");
        }
    }
    class Gun : Weapon//derived class
    {
        public Gun() { Debug.Log("Gun constructed"); weaponDamage = 20; weaponRange = 10; }
        public override void Attack()// Overriding the base class method
        {
            Debug.Log("Gun shoots");
        }
    }


}