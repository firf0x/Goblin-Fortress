using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goblin_Fortress.Components
{
    /// <summary>    
    /// A class that stores all classes inheriting Component.
    /// </summary>
    public class ListComponents
    {
        private readonly List<Component> _components;
        public ListComponents() 
        {
            _components = new List<Component>();
        }

        /// <summary>
        /// The method for getting the component from the list.
        /// </summary>
        /// <typeparam name="T"> Which component will be received. </typeparam>
        public Component GetComponent<T>() where T : Component
        {
            foreach (var component in _components)
            {
                if(component is T) return component;
            }
            return null;
        }

        /// <summary>
        /// The method that will add new components to the list.
        /// </summary>
        /// <typeparam name="T"> Which component should be added to the list. </typeparam>
        /// <param name="newComponent"> The element to be added. </param>
        public void AddComponent<T>(T newComponent) where T : Component
        {
            if (newComponent != null) return;
            _components.Add(newComponent);
        }

        /// <summary>
        /// A method that removes a component from the list.
        /// </summary>
        /// <typeparam name="T"> Which component should be removed from the list. </typeparam>
        /// <param name="oldComponent"> The element that is being deleted. </param>
        public void DelComponent<T>(T oldComponent) where T : Component
        {
            if (!_components.Contains(oldComponent) && oldComponent != null) return;
            _components.Remove(oldComponent);
        }
        
        /// <summary>
        /// Get array components.
        /// </summary>
        public Array GetArrayComponents()
        {
            return _components.ToArray();
        }
        
        /// <summary>
        /// Get list components.
        /// </summary>
        public List<Component> GetListComponents()
        {
            return _components;
        }
    }
}
