using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class StringListSearchProvider : ScriptableObject, ISearchWindowProvider
{
    private IEnumerable<string> templates;
    private Action<string> OnSelected;

    List<SearchTreeEntry> ISearchWindowProvider.CreateSearchTree(SearchWindowContext context)
    {
        List<SearchTreeEntry> searchList = new List<SearchTreeEntry>();
        searchList.Add(new SearchTreeGroupEntry(new GUIContent("Templates")));

        List<string> sortedTemplateList = templates.ToList();
        sortedTemplateList.Sort((a, b) =>
        {
            string[] splits1 = a.Split('/');
            string[] splits2 = b.Split('/');
            for (int i = 0; i < splits1.Length; i++)
            {
                if(i >= splits2.Length)
                {
                    return 1;
                }
                int value = splits1[i].CompareTo(splits2[i]);
                if(value != 0)
                {
                    if (splits1.Length != splits2.Length && (i == splits1.Length - 1 || i == splits2.Length - 1))
                        return splits1.Length < splits2.Length ? 1 : -1;
                    return value;
                }
            }

            return 0;
        });

        List<string> groups = new List<string>();
        foreach(string item in sortedTemplateList)
        {
            string[] entryTitle = item.Split('/');
            string groupName = "";
            for(int i = 0; i < entryTitle.Length - 1; i++)
            {
                groupName += entryTitle[i];
                if (!groups.Contains(groupName))
                {
                    searchList.Add(new SearchTreeGroupEntry(new GUIContent(entryTitle[i]), i + 1));
                    groups.Add(groupName);
                }
                groupName += "/";
            }
            SearchTreeEntry entry = new SearchTreeEntry(new GUIContent(entryTitle.Last()));
            entry.level = entryTitle.Length;
            entry.userData = entryTitle.Last();
            searchList.Add(entry);
        }

        return searchList;
    }

    bool ISearchWindowProvider.OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        OnSelected?.Invoke((string)SearchTreeEntry.userData);
        return true;
    }

    public void Init(IEnumerable<string> templates, Action<string> OnSelected)
    {
        this.templates = templates;
        this.OnSelected = OnSelected;
    }
}
