   M S B A M L   `   `   `  ����E  ArecipesApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:recipesApp.WindowsrecipesApp.Windows  5   	   )    #recipesApp.Windows.FavouritesWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:recipesApp.Windows     5   	     � Title$  FavouritesWindow��5   	   $	��450��6"   $	��800��6/   .��5	      � .+�5
   
   � -   5        $
��Main��5
      .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ datagrid��5      datagrid��   SelectionMode$ Single=�6:   $��21,89,385,46q�6Q      AutoGenerateColumns .  ��6g   �� *	recipes+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6B   -   5        name��   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 5   ^   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   �� *name+6^     5      $ 	prep time��6G   -   5        preparation_time��   
IsReadOnly . $
 Auto 5   �    6)   �� *	prep_time+6�     5      $ num of serves��6H   -   5        	num_serve�� . $
 Auto 5   �    6)   �� *
Num_serves+6�   4  .System.Windows.Controls.DataGridTemplateColumn  5      $ Details��6-   	   CellTemplate	 5      �� 5      .R�5   "   �� -   5        $
��View��5   )   ��6K   �� *id+5      5        5      $ delete��6-   	 5      �� 5      .R�5   "   �� -   5        $��Delete��5   )   6:   5      5      5       �� 5!      -   5        $
��Back��5!      $
��Left=�6$   $��680,388,0,0q�6?   $	��Top=�6T   $��75��6l   6w   �� 5"      -	   5        $��search��5"      $
��Left=�6$   $��23��6?   $��
241,50,0,0q�6K   
 �� TextWrapping$

 Wrap=�6_   $�� ��6s   $	��Top=�6{   $	��120��6�   6�   �� 5#      $��Search��6   $
��Left=�6%   $��
198,50,0,0q�6@   $	��Top=�6T   5%      � 5&   
   -
   5        $
��View��5&      $��	10,10,0,0q�6   .+�5'      �� -   5         ��Name$ ingredients��5'      $
��Left=�6+   $	��334��6F   $��
546,28,0,0q�6S   $	��Top=�6g   $	��208��6   �� 5(      -   5        $��Close��5(      $
��Left=�6%   $��443,367,0,0q�6@   $	��Top=�6U   $	��178��6m   $��33��6�   �� 5)      -   5        $��Method��5)      $
��Left=�6(   $	��241��6C   $��
10,121,0,0q�6P   $	��Top=�6d   $	��513��6|   . �5*      /� .T�5+      J� .*�6    .*�5,      �� $� ��6   5-      5.      5/      �� 50      -   5        $
��Name��50      $
��Left=�6%   $��	28,28,0,0q�6@   $
��Wrap=�6S   $
��Name��6g   $	��Top=�6s    J� RenderTransformOrigin$ 0.5,0.5&�6�   51      �� 52      -   5        $��	Prep_time��52      $
��Left=�6)   $��	28,57,0,0q�6D   $
��Wrap=�6W   $��	prep time��6k   $	��Top=�6|   $ 0.447,0.877&�6�   �� 53      -   5        $��
Num_serves��53      $
��Left=�6*   $��	28,90,0,0q�6E   $
��Wrap=�6X   $��Number of servies��6l   $	��Top=�6�   55      56      57      