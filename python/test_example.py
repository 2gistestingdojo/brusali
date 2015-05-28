from base_test_case import BaseTestCase
import unittest


class ExampleTest(BaseTestCase):
    def test_example(self):
        main_window = self.driver.find_element_by_id('MainWindow')

        search_string = main_window.find_element_by_id('QueryMW')
        search_string.send_keys('3')

        search_button = main_window.find_element_by_id('SearchMW')
        search_button.click()

        products_list = main_window.find_element_by_id('ProductsMW')
        product_items = products_list.find_elements_by_class_name('ListViewItem')

        self.assertEqual(len(product_items), 1)


if __name__ == '__main__':
    unittest.main()
