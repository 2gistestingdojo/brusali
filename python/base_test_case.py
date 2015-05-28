from selenium import webdriver
import unittest
import os


class BaseTestCase(unittest.TestCase):
    def setUp(self):
        apps_folder = os.environ.get('UITestApps', 'C:\\app')
        kwargs = dict(
            command_executor='http://127.0.0.1:9999',
            desired_capabilities={
                'app': os.path.join(apps_folder, 'TestingDojo2015.exe')
            })

        self.driver = webdriver.Remote(**kwargs)

    def tearDown(self):
        self.driver.quit()
