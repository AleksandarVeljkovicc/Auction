   M S B A M L   `   `   `  ����E  AAuction.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Auction.UI
Auction.UI  5   	   L HAuction.ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullpJclr-namespace:Auction.ViewModel.Custom_controls;assembly=Auction.ViewModel!Auction.ViewModel.Custom_controls 60       Auction.UI.LoginWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Auction.UI     5   	   fcustom_controlsJclr-namespace:Auction.ViewModel.Custom_controls;assembly=Auction.ViewModel     60     � Title$  LoginWindow��5   	   $	��450��6   $	��800��6*    � WindowStartupLocation$ CenterScreen=�67    � 
ResizeMode$ NoResize=�6\   .��5	      �  � ColumnDefinitions 5
   
   �� 5      6!   �� 5      6!   �� 5      $��4* �6   6,   �� 5      6!   �� 5      6!    � RowDefinitions 5   
   �� 5      6   �� 5      6   �� 5      $��8* �6   6*   �� 5      $
��1.5* �6   6,   �� 5      6   .+�5   
   �� $��2��6   $��2��6   .+�5      ��  �� RadiusX$ 5��6    �� RadiusY$ 5��6$   $	��300��60   $	��225��6<   $��47,30,30,30q�6I   ��5      �� 5      .��5      �  � Color$
 Gray��6'    � Offset$ 0Q�64   � 5      $ white��6'   $	 0.6Q�65   5      5       �� 5!      
	 �� Top$	 40��6   
 �� Left$
 65��6$   $��30(�65   
���   �.��6V   Log in6^   �� 5"      -   5        $��txtBox��5"      
�������$��16(�6h   $		 100��6v   $
 65��6�   $	��262��6�   $��30��6�   ��6$   �� *
Username+ �� FallbackValue$ Username��6�   ;  5Auction.ViewModel.Custom_controls.BindablePasswordBox  5#      -   5        $��pwBox��5#   i   $��16(�6v   $		 150��6�   $
 65��6�   $	��262��6�   $��30��6�      Password 62   �� *
Password+$ Password��6�   �� 5$      -   5        $��Log in��5$      $		 200��6&   $
 65��67   $	��262��6H   $��40��6T   6w   5%      � 5&   
   $��3��6   $��2��6   .+�5'      �� -   5        $��msgLabel��5'      $
��Wrap=�6(   $��Center=�6<   $
��Bold"�6Y    �� TextAlignment$ Center=�6�   ��6k   �� *ErrorMessage+$ Welcome to login page��6�   5(      5)      5*      